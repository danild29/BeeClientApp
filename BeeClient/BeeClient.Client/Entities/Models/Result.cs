using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeeClient.Client.Entities.Models;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error)
        :base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess ? _value : default;

    //public static implicit operator TValue(Result<TValue> result) => result.Value;

    public static implicit operator Result<TValue>(TValue value) => 
        new Result<TValue>(value, true, Error.None);


    public static Result<TValue> Success(TValue? value) => new(value, true, Error.None);

    public TReturn Match<TReturn>(Func<TValue, TReturn> onSucc, Func<Error, TReturn> onFail)
    {
        return IsSuccess ? onSucc(Value): onFail(Error);
    }

    public void Match(Action<TValue> onSucc, Action<Error> onFail)
    {
        if (IsSuccess) onSucc(Value);
        else onFail(Error);
    }
}

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; set; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("неверная ошибка", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);

}

public class Error
{
    public HttpResponseMessage? HttpResponse { get; set; }
    public string Content { get ; set; }

    public static Error CreateCustom(string content)
    {
        return new Error(null, content);
    }

    public static async Task<Error> Create(HttpResponseMessage HttpResponse)
    {
        string content = await HttpResponse.Content.ReadAsStringAsync();
        return new Error(HttpResponse, content);
    }

    public Error(HttpResponseMessage? httpResponse)
    {
        HttpResponse = httpResponse;
    }

    private Error(HttpResponseMessage? httpResponse, string content)
    {
        HttpResponse = httpResponse;
        Content = content;
    }


    public T ParseTo<T>()
    {
        return JsonConvert.DeserializeObject<T>(Content);
    }


    public static readonly Error None = new Error(null);
    public static implicit operator Result(Error error) => Result.Failure(error);
}

