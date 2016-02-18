using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class ApplicationRoot : ContextView
{
	
	void Awake()
	{
		//Instantiate the context, passing it this instance.
		context = new DesertContext(this);
		
		//This is the most basic of startup choices, and probably the most common.
		//You can also opt to pass in ContextStartFlag options, such as:
		//
		//context = new MyFirstContext(this, ContextStartupFlags.MANUAL_MAPPING);
		//context = new MyFirstContext(this, ContextStartupFlags.MANUAL_MAPPING | ContextStartupFlags.MANUAL_LAUNCH);
		//
		//These flags allow you, when necessary, to interrupt the startup sequence.
	}
}
