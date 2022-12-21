namespace DesktopSetupConfigurator.Database.Models.Common;
public abstract class DbModel
{
	public int Id { get; protected set; } = default!;
	public DateTimeOffset CreatedOn { get; protected set; } = DateTimeOffset.Now;
	public DateTimeOffset UpdatedOn { get; protected set; } = DateTimeOffset.Now;
	public bool? IsDeleted { get; protected set; }
	public DateTimeOffset? DeletedOn { get; protected set; }

	public void Delete()
	{
		DeletedOn = DateTimeOffset.Now;
		IsDeleted = true;
	}

	public void Update()
	{
		UpdatedOn = DateTimeOffset.Now;
	}
}
