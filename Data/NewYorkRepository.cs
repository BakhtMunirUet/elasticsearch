using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Expressions;
using Nest;

namespace API.Data
{
    public class NewYorkRepository : INewYorkRepository
    {
        private readonly IMapper _mapper;

        private readonly ElasticClient _client;
        public NewYorkRepository(IMapper mapper)
        {
            _client = ClientExtensions.IndexName(IndexNames.NewYork);
            _mapper = mapper;
        }


        public async Task<bool> AddData(NewYorkAddDtos newYorkAddDtos)
        {
            ISearchResponse<NewYork> searchResponse = await Exists(newYorkAddDtos.id);
            if (searchResponse != null)
            {

                var updateResponse = await _client.UpdateAsync<NewYorkAddDtos>(
                    DocumentPath<NewYorkAddDtos>.Id(searchResponse.Hits.FirstOrDefault().Id),
                        descriptor => descriptor
                        .RetryOnConflict(3)
                        .Doc(newYorkAddDtos)
                            .Refresh(Refresh.True)
                );

                return updateResponse.IsValid;
            }
            else
            {
                var addResponse = await _client.IndexDocumentAsync<NewYorkAddDtos>(newYorkAddDtos);
                return addResponse.IsValid;
            }
        }

        public async Task<bool> AddManyBulk(List<NewYorkAddDtos> newYorkList)
        {
            List<NewYorkAddDtos> tempList = new List<NewYorkAddDtos>();

            foreach (var newYork in newYorkList)
            {

                ISearchResponse<NewYork> searchResponse = await Exists(newYork.id);
                if (searchResponse != null)
                {
                    var updateResponse = await _client.UpdateAsync<NewYorkAddDtos>(
                            DocumentPath<NewYorkAddDtos>.Id(searchResponse.Hits.FirstOrDefault().Id),
                                descriptor => descriptor
                                .RetryOnConflict(3)
                                .Doc(newYork)
                                    .Refresh(Refresh.True)
                    );
                }
                else
                {
                    tempList.Add(newYork);
                }
            }

            if (tempList.Count > 0)
            {
                var indexManyAsyncResponse = await _client.IndexManyAsync(tempList);
                return indexManyAsyncResponse.IsValid;
            }
            else
            {
                return true;
            }

        }

        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetNewYorkAllDataAsync(UserParams userParams)
        {
            List<NewYorkSecondDtos> newYorkList = new List<NewYorkSecondDtos>();
            var searchResponse = await _client.SearchAsync<NewYork>(s => s
                .From(userParams.SkipCount)
                .Size(userParams.MaxResultCount)
                .Query(q => q
                    .MatchAll()
                )
            );
            newYorkList = _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(searchResponse.Documents.ToList());
            return newYorkList;
        }

        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetNewYorkSpecificDataAsync(string name)
        {
            var searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.name)
                            .Query(name)
                        )
                    )
            );

            return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(searchResponse.Documents.ToList());
        }

        public async Task<ActionResult<NewYork>> GetFindById(int id)
        {
            var searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .Bool(m => m
                        .Must(
                            bs => bs.Term(p => p.id, id)
                        )
                    )
                )
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.FirstOrDefault();
            }
            return null;


        }

        public async Task<bool> UpdateData(NewYorkUpdateDtos newYorkUpdateDtos, int id)
        {

            ISearchResponse<NewYork> searchResponse = await Exists(id);

            if (searchResponse != null)
            {
                var newYork = searchResponse.Documents.FirstOrDefault();
                var updateResponse = await _client.UpdateAsync<NewYorkUpdateDtos>(
                        DocumentPath<NewYorkUpdateDtos>.Id(searchResponse.Hits.FirstOrDefault().Id),
                            descriptor => descriptor
                            .RetryOnConflict(3)
                            .Doc(newYorkUpdateDtos)
                                .Refresh(Refresh.True)
                );

                if (updateResponse.IsValid)
                {
                    return true;
                }

                return false;
            }



            return false;
        }

        public async Task<bool> DeleteData(int id)
        {

            ISearchResponse<NewYork> searchResponse = await Exists(id);

            if (searchResponse != null)
            {

                var newYork = searchResponse.Documents.FirstOrDefault();
                String deletedId = searchResponse.Hits.FirstOrDefault().Id;
                var updateResponse = await _client.DeleteAsync<NewYork>(deletedId);
                if (updateResponse.IsValid)
                {
                    return true;
                }

                return false;
            }

            return false;
        }


        private async Task<ISearchResponse<NewYork>> Exists(int id)
        {
            var searchResponse = await _client.SearchAsync<NewYork>(s => s
              .Query(q => q
                  .Bool(m => m
                      .Must(
                          bs => bs.Term(p => p.id, id)
                      )
                  )
              )
            );

            if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
            {
                return searchResponse;
            }
            return null;
        }
    }
}