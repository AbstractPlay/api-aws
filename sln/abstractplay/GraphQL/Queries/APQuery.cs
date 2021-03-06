using System;
using System.Linq;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

using abstractplay.DB;

namespace abstractplay.GraphQL
{
    /*
     * This query should only contain data available to unauthenticated users.
     */
    public class APQuery : ObjectGraphType
    {
        public APQuery(MyContext db)
        {
            Field<UserType>(
                "user",
                description: "A single user record",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: _ =>
                {
                    var id = _.GetArgument<string>("id");
                    return db.Owners.SingleOrDefault(x => x.OwnerId.Equals(GuidGenerator.HelperStringToBA(id)) && !x.Anonymous);
                }
            );
            Field<ListGraphType<UserType>>(
                "users",
                description: "List of registered users",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "country" }),
                resolve: _ =>
                {
                    var country = _.GetArgument<string>("country");
                    if (String.IsNullOrWhiteSpace(country))
                    {
                        return db.Owners.Where(x => !x.Anonymous).ToArray();
                    }
                    else
                    {
                        return db.Owners.Where(x => x.Country.Equals(country) && !x.Anonymous).ToArray();
                    }
                }
            );
            Field<GamesMetaType>(
                "gameMeta",
                description: "A single game's metadata",
                arguments: new QueryArguments(new[] {
                    new QueryArgument<StringGraphType> { Name = "id", Description = "The game's unique id" }, 
                    new QueryArgument<StringGraphType> { Name = "shortcode", Description = "The game's shortcode" } 
                }),
                resolve: _ =>
                {
                    var id = _.GetArgument<string>("id");
                    var shortcode = _.GetArgument<string>("shortcode");
                    if (!String.IsNullOrWhiteSpace(id))
                    {
                        return db.GamesMeta.SingleOrDefault(x => x.GameId.Equals(GuidGenerator.HelperStringToBA(id)));

                    } else if (!String.IsNullOrWhiteSpace(shortcode))
                    {
                        return db.GamesMeta.SingleOrDefault(x => x.Shortcode.Equals(shortcode));
                    } else 
                    {
                        throw new ExecutionError("You must provide either the game's unique ID or its shortcode.");
                    }
                }
            );
            Field<ListGraphType<GamesMetaType>>(
                "gamesMeta",
                description: "Metadata for multiple games",
                arguments: new QueryArguments(new[] {
                    new QueryArgument<StringGraphType> { Name = "tag", Description = "A tag to search for" }
                }),
                resolve: _ =>
                {
                    var tag = _.GetArgument<string>("tag");
                    if (!String.IsNullOrWhiteSpace(tag))
                    {
                        List<GamesMeta> retlst = new List<GamesMeta>();
                        foreach (var rec in db.GamesMetaTags.Where(x => x.Tag.Equals(tag)))
                        {
                            retlst.Add(rec.Game);
                        }
                        return retlst;
                    }
                    else
                    {
                        return db.GamesMeta.ToArray();
                    }
                }
            );
            Field<ChallengeType>(
                "challenge",
                description: "A specific challenge",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: _ =>
                {
                    var id = _.GetArgument<string>("id");
                    return db.Challenges.SingleOrDefault(x => x.ChallengeId.Equals(GuidGenerator.HelperStringToBA(id)));
                }
            );
            Field<ListGraphType<ChallengeType>>(
                "challenges",
                description: "A list of all challenges",
                resolve: _ => db.Challenges.ToArray()
            );
            Field<ListGraphType<GamesDataType>>(
                "games",
                description: "All games in progress",
                resolve: _ => db.GamesData.Where(x => x.Closed.Equals(false)).ToArray()
            );
        }
    }
}