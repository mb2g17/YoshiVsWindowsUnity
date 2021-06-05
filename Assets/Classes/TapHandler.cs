using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// Handles tapping of axis
    /// </summary>
    class TapHandler
    {
        /// <summary>
        /// Stores tap state
        /// 0 - not pressed up at all
        /// 1 - tapped up
        /// 2 - holding down up, but no longer tapped
        /// </summary>
        private int _tapState = 0;

        /// <summary>
        /// Predicate that determines what holding down the button is like
        /// </summary>
        private Func<bool> _buttonDownPredicate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buttonDownPredicate">When true, button is held down</param>
        public TapHandler(Func<bool> buttonDownPredicate)
        {
            _buttonDownPredicate = buttonDownPredicate;
        }

        /// <summary>
        /// Gets if we've tapped this axis
        /// </summary>
        /// <returns>True if we've tapped, false if not</returns>
        public bool IsTapped()
        {
            return _tapState == 1;
        }

        /// <summary>
        /// Updates the tap handler
        /// </summary>
        public void Update()
        {
            // If we've let go of up after holding down up, go back to initial state
            if (!_buttonDownPredicate() && _tapState == 2)
            {
                _tapState = 0;
            }

            // If we've tapped up before, reset back
            if (_tapState == 1)
            {
                _tapState = 2;
            }

            // If we're pressing up AND we haven't tapped up yet, tap up
            if (_buttonDownPredicate() && _tapState == 0)
            {
                _tapState = 1;
            }
        }
    }
}
