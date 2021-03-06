﻿using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;

namespace cse3902.Rooms
{
    public class RoomTransitionManager
    {
        private Game1 game;
        
        private List<float> steps;
        private int stepTracker;

        private IDoor entranceDoor;

        private int linkNewRoomStepsRemaining;
        private Vector2 linkNewRoomDirection;

        public RoomTransitionManager(Game1 game)
        {
            this.game = game;

            InitializeSteps();
            stepTracker = steps.Count-1;
        }
        private void InitializeSteps()
        {
            steps = new List<float>();

            // ADD ALL STEPS BELOW HERE IN INCREASING ORDER (<10.0)

            steps.Add(0.0f);
            steps.Add(1.0f);
            steps.Add(9.99f);

            // ADD ALL STEPS ABOVE HERE

            steps.Add(10.0f); //this indicates end of the manager - DO NOT CHANGE
        }
        public void StartTransitionManager(IDoor entrance)
        {
            stepTracker = 0;
            entranceDoor = entrance;
            game.CollisionManager.Disabled = true;
            game.Player.ChangeDirection(new Vector2(0,0));
            RoomBlocks.Instance.Reset();
        }
        public bool IsTransitioning()
        {
            return steps[stepTracker] != 10.0f;
        }

        public void Update()
        {
            switch (steps[stepTracker])
            {
                case 0.0f:
                    if (!game.Camera.IsCameraMoving()) MoveLinkToNewRoom();
                    break;

                case 1.0f:
                    MoveLinkIntoRoom();
                    break;

                case 9.99f:
                    game.CollisionManager.Disabled = false;
                    game.RoomHandler.CompleteStart();
                    stepTracker++;
                    break;

                case 10.0f: //manager is complete/inactive - DO NOT CHANGE
                    break;

                default: //this should never happen
                    break;
            }
        }

        private void MoveLinkToNewRoom()
        {
            game.Player.Center = entranceDoor.PlayerReleasePosition();
            linkNewRoomDirection = entranceDoor.PlayerReleaseDirection();
            linkNewRoomStepsRemaining = ((int) linkNewRoomDirection.Length()) - 1;
            linkNewRoomDirection.Normalize();
            game.Player.ChangeDirection(linkNewRoomDirection);
            stepTracker++;
            if (linkNewRoomStepsRemaining == 0)
            {
                stepTracker++;
                game.Player.ChangeDirection(new Vector2(0, 0));
            }
        }

        private void MoveLinkIntoRoom()
        {
            game.Player.ChangeDirection(linkNewRoomDirection);
            linkNewRoomStepsRemaining--;
            if (linkNewRoomStepsRemaining == 0)
            {
                stepTracker++;
                game.Player.ChangeDirection(new Vector2(0, 0));
            }
        }
    }
}
