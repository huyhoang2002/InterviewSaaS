﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.QueryDTO
{
    public record InterviewCollectionDTO(
            Guid Id,
            string CollectionName,
            bool IsApplied
        );
}
