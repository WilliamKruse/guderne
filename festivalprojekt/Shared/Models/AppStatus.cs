using System;
namespace festivalprojekt.Shared.Models
    
{
	public class AppStatus
	{

        public bool LoggedIn { get; private set; }

        public event Action OnChange;

        public void Log(bool nool)
        {
            LoggedIn = nool;
          
            NotifyStateChanged();
        }

        //opdater siden på 
        private void NotifyStateChanged() => OnChange?.Invoke();
      
	}
}

