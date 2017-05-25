using System;

public interface StoryActionEvent {
	void Activate(System.Action callback);
}