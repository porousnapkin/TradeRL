
public class IncreaseEffortPoolSkillLevelBenefit : SkillLevelBenefit
{
	public Effort.EffortType type;
	public int amount;

	public override void Apply(PlayerCharacter plapyerCharacter)
	{
		var increaser = DesertContext.StrangeNew<EffortPoolIncreaser>();
		increaser.type = type;
		increaser.amount = amount;
		increaser.Run();
	}
}

public class EffortPoolIncreaser
{
	[Inject]public Effort effort {private get; set;}
	public Effort.EffortType type;
	public int amount;

	public void Run() 
	{
		effort.SetMaxEffort(type, effort.GetMaxEffort(type) + amount);
	}
}
