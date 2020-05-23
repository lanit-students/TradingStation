using System;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using IDeleteUserUserService.Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserService.Commands
{
    public class DeleteUserCommand : IDeleteUserCommand
     {
        private readonly IRequestClient<InternalDeleteUserRequest> client;
        private readonly IValidator<DeleteUserRequest> validator;
        private ILogger<DeleteUserCommand> logger;

        public DeleteUserCommand(
            [FromServices]IRequestClient<InternalDeleteUserRequest> client,
            [FromServices] IValidator<DeleteUserRequest> validator,
            [FromServices] ILogger<DeleteUserCommand> logger)
        {
            this.client = client;
            this.validator = validator;
            this.logger = logger;
        }

        private async Task<bool> DeleteUser(InternalDeleteUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(DeleteUserRequest request)
         {
            validator.ValidateAndThrow(request);

            var user = new InternalDeleteUserRequest { UserId = request.UserId };

            try
            {
                await DeleteUser(user);
                return true;
            }
            catch (NotFoundException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new NotFoundException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
            catch (BadRequestException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new BadRequestException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
        }
     }
 }
