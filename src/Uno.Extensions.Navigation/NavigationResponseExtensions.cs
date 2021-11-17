﻿using System.Threading.Tasks;

namespace Uno.Extensions.Navigation;

public static class NavigationResponseExtensions
{
    public static NavigationResultResponse? AsResult(this NavigationResponse response)
    {

        if (response is NavigationResultResponse genericResultResponse)
        {
            return genericResultResponse;
        }

        return null;
    }

    public static NavigationResultResponse<TResult>? AsResult<TResult>(this NavigationResponse response)
    {
        if (response is NavigationResultResponse<TResult> resultResponse)
        {
            return resultResponse;
        }

        if (response is NavigationResultResponse genericResultResponse)
        {
            return new NavigationResultResponse<TResult>(response.Route,
                genericResultResponse.Result.ContinueWith(x =>
                    (x.Result.MatchSome(out var val) && val is TResult tval) ?
                        Options.Option.Some(tval) :
                        Options.Option.None<TResult>(),
                    TaskScheduler.Current));
        }

        return null;
    }
}