public interface Controller {
	void BeginTurn(System.Action turnFinished);
	Character GetCharacter();
}