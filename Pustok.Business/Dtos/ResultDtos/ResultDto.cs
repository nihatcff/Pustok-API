using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Dtos;

public class ResultDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSucced { get; set; }

    public ResultDto()
    {
        StatusCode = 200;
        Message = "Successfully";
        IsSucced = true;
    }


    public ResultDto(string message)
    {
        Message = message;
    }

    public ResultDto(string message, int statusCode, bool isSucced)
    {
        StatusCode = statusCode;
        Message = message;
        IsSucced = isSucced;
    }

    public ResultDto(int statusCode, bool isSucced)
    {
        StatusCode = statusCode;
        IsSucced = isSucced;
    }

}


public class ResultDto<T> : ResultDto
{
    public T? Data { get; set; }
}


