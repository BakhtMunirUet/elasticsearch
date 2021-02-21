using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace API.Data
{
    public class BmkRepository : IBmkRepository
    {

        private readonly ElasticClient _client;

        public BmkRepository()
        {
            _client = ClientExtensions.IndexName(IndexNames.Bmk);
        }

        public async Task<ActionResult<string>> CreateIndex()
        {
            var createIndexResponse = await _client.Indices.CreateAsync(IndexNames.Bmk, c => c
               .Map<BmkAttributeMapping>(m => m.AutoMap())
            );

            if (createIndexResponse.IsValid)
            {
                return createIndexResponse.Index;
            }
            return createIndexResponse.ServerError.Error.Type;
        }

        public async Task<ActionResult<string>> CreateManualIndex()
        {

            var createIndexResponse = await _client.Indices.CreateAsync(IndexNames.BmkManual, c => c
               .Settings(s => s
                    .NumberOfShards(3)
                    .NumberOfReplicas(1)
               )
               .Map<Bmk>(m => m
                    .Properties(ps => ps
                        .Text(s => s
                            .Name(e => e.first_name)
                        )
                        .Text(s => s
                            .Name(e => e.last_name)
                        )
                        .Number(n => n
                            .Name(e => e.salary)
                        )
                        .Date(d => d
                            .Name(e => e.birthday)
                            .Format("MMddyyyy")
                        )
                    )
               )
            );

            if (createIndexResponse.IsValid)
            {
                return createIndexResponse.Index;
            }
            return createIndexResponse.ServerError.Error.Type;

        }

        public async Task<ActionResult<string>> CreateNGramsSettingIndex()
        {

            var nGramFilters = new List<string> { "lowercase", "asciifolding", "nGram_filter" };
            var createIndexResponse = await _client.Indices.CreateAsync(IndexNames.BmkNGramIndex, c => c
               .Settings(s => s
                    .NumberOfShards(3)
                    .NumberOfReplicas(1)
                    .Setting(UpdatableIndexSettings.MaxNGramDiff, 50)
                    .Analysis(a => a
                        .Analyzers(anz => anz
                            .Custom("ngram_analyzer", cc => cc
                                .Tokenizer("ngram_tokenizer")
                                .Filters(
                                    "lowercase"
                                )
                            )
                        )
                        .Tokenizers(tz => tz
                            .NGram("ngram_tokenizer", td => td
                                .MinGram(1)
                                .MaxGram(20)
                                .TokenChars(
                                    TokenChar.Letter,
                                    TokenChar.Digit,
                                    TokenChar.Punctuation,
                                    TokenChar.Symbol
                                )
                            )
                        )
                    )

                )
               .Map<Bmk>(m => m
                    .Properties(ps => ps
                        .Text(s => s
                            .Name(e => e.first_name)
                        )
                        .Text(s => s
                            .Name(e => e.last_name)
                        )
                        .Number(n => n
                            .Name(e => e.salary)
                        )
                        .Date(d => d
                            .Name(e => e.birthday)
                            .Format("MMddyyyy")
                        )
                    )
               )
            );

            if (createIndexResponse.IsValid)
            {
                return createIndexResponse.Index;
            }
            return createIndexResponse.ServerError.Error.Type;

        }
    }
}