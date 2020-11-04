﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ZombieMaze {

	public class CameraControl : MonoBehaviour {

		public float yMin = 5;
		public float ySpeed = 5;

		public const float SMOOTH_DAMP_DURATION = .1f;

		public void InitialSetUp() {

			// reposition camera
			Camera.main.transform.position = new Vector3 (
				0,//Maze.instance.Width * Maze.TILE_SPACING / 2,
				20,
				0//Maze.instance.Length * Maze.TILE_SPACING / 2
			);

			// initial tilt:
			Camera.main.transform.rotation = Quaternion.Euler(45, 0, 0);

			target = Camera.main.transform.position;

		}

		// Use this for initialization
		void Start () {
			//move this out later?
			InitialSetUp();
		}
		
		// Update is called once per frame
		void Update () {
			
			// control height with arrow keys
			if (Input.GetKey (KeyCode.DownArrow)) {
				target.y += ySpeed * Time.unscaledDeltaTime;
			} else if (Input.GetKey (KeyCode.UpArrow)) {
				target.y -= ySpeed * Time.unscaledDeltaTime;
			}
			target.y = Mathf.Max(yMin, target.y);

			// hover over ball
			//TODO: Get player position from entity
			
			target.x = playerPosition.x;
			target.z = playerPosition.z;
			

			// offset for camera rotation
			float zOff = (target.y - playerPosition.y) / Mathf.Tan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Rad2Deg);
			target.z += zOff;

			// applying smooth damp
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref camVel, SMOOTH_DAMP_DURATION, float.MaxValue, Time.unscaledDeltaTime);

		}

		[HideInInspector] public float3 playerPosition;

		private Vector3 target = new Vector3();
		private Vector3 camVel = new Vector3();
	}

}