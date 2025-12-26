using MediatR;

public sealed record DeleteContactCommand(Guid id) : IRequest;