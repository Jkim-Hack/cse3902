using System;
namespace cse3902.Entities
{
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
