﻿using X.Application.Core.CQRS;

namespace X.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : ICommand<Guid>;
