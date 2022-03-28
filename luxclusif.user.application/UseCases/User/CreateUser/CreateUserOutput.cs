using DomainEntity = luxclusif.user.domain.Entity;
namespace luxclusif.user.application.UseCases.User.CreateUser;
public class CreateUserOutput
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastUpdateAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public CreateUserOutput(Guid id, string name, DateTimeOffset createdAt, DateTimeOffset? lastUpdateAt, DateTimeOffset? deletedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        LastUpdateAt = lastUpdateAt;
        DeletedAt = deletedAt;
    }

    public static CreateUserOutput FromUser(DomainEntity.User entity)
    => new(
            entity.Id,
            entity.Name,
            entity.CreatedAt,
            entity.LastUpdateAt,
            entity.DeletedAt
            );
}
