using System;
namespace cse3902.Entities
{
    // Link state machine 
    public class LinkStateMachine
    {
        private bool facingLeft = true;

        public LinkStateMachine()
        {

        }

        public void ChangeDirection()
        {
            facingLeft = !facingLeft;
        }

    }
}
