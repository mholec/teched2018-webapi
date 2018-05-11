﻿namespace Teched2018.Services
{
    public static class ValidationService
    {
	    public static bool ValidateTitle(string title, out string errorMessage)
	    {
		    errorMessage = null;

		    if (title.StartsWith("C"))
		    {
			    errorMessage = "Title cannot starts with C";
			    return false;
		    }

		    return true;
	    }
    }
}
