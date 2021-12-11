﻿namespace DeveloperTest.Application.Validators
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T instance);
    }
}
