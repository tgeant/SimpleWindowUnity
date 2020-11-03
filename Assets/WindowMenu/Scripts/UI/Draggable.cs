using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	private bool isDrag = false;
	private bool isOverlap = false;

	// Callbacks on event
	private Action actionOnClick;
	private Action actionOnClickRelease;

	private Action actionOnMouseEnter;
	private Action actionOnMouseExit;


	/*
	 * Pointers Handlers (Main functions)
	 */

	public void OnPointerDown(PointerEventData eventData)
	{
		isDrag = true;
		
		if(actionOnClick != null)
			actionOnClick.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDrag = false;

		if (actionOnClickRelease != null)
			actionOnClickRelease.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isOverlap = true;

		if (actionOnMouseEnter != null)
			actionOnMouseEnter.Invoke();
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		isOverlap = false;

		if (actionOnMouseExit != null)
			actionOnMouseExit.Invoke();
	}


	/*
	 * Getters
	 */

	public bool isDragging()
	{
		return isDrag;
	}

	public bool isOverlapping()
	{
		return isOverlap;
	}


	/*
	 * Action setters (callbacks handlers)
	 */
	public void SetActionOnClick(Action actionOnClick)
	{
		this.actionOnClick = actionOnClick;
	}
	
	public void SetActionOnClickRelease(Action actionOnClickRelease)
	{
		this.actionOnClickRelease = actionOnClickRelease;
	}
	
	public void SetActionOnMouseEnter(Action actionOnMouseEnter)
	{
		this.actionOnMouseEnter = actionOnMouseEnter;
	}
	
	public void SetActionOnMouseExit(Action actionOnMouseExit)
	{
		this.actionOnMouseExit = actionOnMouseExit;
	}

}
