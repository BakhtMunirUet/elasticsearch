using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace API.Data
{
    public class GeneralRepository : IGeneralRepository
    {

        private readonly IMapper _mapper;

        private readonly ElasticClient _client;
        public GeneralRepository(IMapper mapper)
        {
            _client = ClientExtensions.IndexName(IndexNames.NewYork);
            _mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<NewYork>>> GetAllDataSort(SortParams sortParams)
        {

            ISearchResponse<NewYork> searchResponse;

            if (sortParams.SortBy == "asc")
            {
                searchResponse = await _client.SearchAsync<NewYork>(s => s
                       .From(sortParams.SkipCount)
                       .Size(sortParams.MaxResultCount)
                       .Query(q => q
                           .MatchAll()
                       )
                       .Sort(s => s.Ascending(sortParams.SortParameter))
                );
            }
            else
            {
                searchResponse = await _client.SearchAsync<NewYork>(s => s
                    .From(sortParams.SkipCount)
                    .Size(sortParams.MaxResultCount)
                    .Query(q => q
                        .MatchAll()
                    )
                    .Sort(s => s.Descending(sortParams.SortParameter))
                );
            }
            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            return null;

        }



        public async Task<ActionResult<IEnumerable<NewYork>>> GetAllDataFilter()
        {
            ISearchResponse<NewYork> searchResponse;
            searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Match(ms => ms.Field(f => f.name).Field("Bedstuy")

                            )
                        )
                        .Filter(fl => fl
                            .Range(r => r
                                .Field(f => f.price)
                                    .GreaterThanOrEquals(66)
                                    .LessThanOrEquals(68)
                                    .Relation(RangeRelation.Within)
                            )
                        )


                    )
                )

           );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            return null;

        }

        public async Task<ActionResult<IEnumerable<NewYork>>> GetDataFuzzy(string searchText)
        {
            ISearchResponse<NewYork> searchResponse;
            searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .Fuzzy(fu => fu
                        .Field(f => f.name)
                        .Fuzziness(Fuzziness.Auto)
                        .Value(searchText)
                    )
                )
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            return null;
        }

        public async Task<ActionResult<IEnumerable<NewYork>>> GetDataWildcard(string seachText)
        {
            ISearchResponse<NewYork> searchResponse;
            searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .Wildcard(w => w
                        .Field(f => f.name)
                        .Value(seachText + "*")
                    )
                )
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            return null;
        }

        public async Task<ActionResult<IEnumerable<NewYork>>> GetDataMatchPhrasePrefix(string seachText)
        {
            ISearchResponse<NewYork> searchResponse;
            searchResponse = await _client.SearchAsync<NewYork>(s => s
                .Query(q => q
                    .MatchPhrasePrefix(w => w
                        .Field(f => f.name)
                        .Query(seachText)
                        .Slop(10)
                    )
                )
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            return null;
        }
    }
}