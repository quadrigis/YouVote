﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouVote.Common.DomainModel
{
    [Serializable]
    public abstract class ValidatableObject : BaseObject
    {
        public virtual bool IsValid()
        {
            return ValidationResults().Count == 0;
        }

        public virtual ICollection<ValidationResult> ValidationResults()
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults);
            return validationResults;
        }
    }
}
