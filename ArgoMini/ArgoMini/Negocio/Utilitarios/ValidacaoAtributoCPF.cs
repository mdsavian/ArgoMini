using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArgoMini.Negocio.Utilitarios
{

    public class ValidacaoAtributoCPF : ValidationAttribute, IClientValidatable
    {
        public ValidacaoAtributoCPF()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = Util.ValidaCPF(value.ToString());
            return valido;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "ValidacaoAtributoCPF"
            };
        }
    }
}
