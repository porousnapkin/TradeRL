using System.Collections.Generic;

public interface AmbushActivator
{
    void Activate(List<Character> friends, List<Character> foes, System.Action finished);
}