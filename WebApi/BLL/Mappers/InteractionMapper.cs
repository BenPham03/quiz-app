using BLL.ViewModels.Interaction;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class InteractionMapper
    {
        public static Interactions ToInteractionFromCreate(this CreateInteractionRequestVM interactions, string userId)
        {
            return new Interactions
            {
                Id = Guid.NewGuid(),
                Type = interactions.Type,
                CreatedAt = DateTime.Now,
                QuizzId = interactions.QuizzId,
                UserId = userId,
            };
        }
    }
}
