﻿using System.Text.Json;

namespace WebApi.Models
{
    public class ErrorDetail
    {
        public bool Status { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}