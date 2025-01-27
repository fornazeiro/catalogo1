﻿using System.ComponentModel.DataAnnotations;

namespace CatalogoAPI.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null  || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeriaLetra = value.ToString()[0].ToString();

            if (primeriaLetra != primeriaLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula");
            }

            return ValidationResult.Success;
        }

    }
}
