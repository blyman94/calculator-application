namespace CalculatorLibrary;

/// <summary>
/// A parent class for all custom exceptions for the calculator application. 
/// Instead of try-catching each individual exception type, the main program can
/// try-catch calculator exceptions and handle them by displaying the syntax 
/// error and resetting the calculator input.
/// </summary>
public class CalculatorException : Exception
{
    public CalculatorException() : base()
    {

    }

    public CalculatorException(string message) : base(message)
    {

    }

    public CalculatorException(string message, Exception inner) : base(message, inner)
    {

    }

    protected CalculatorException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }
}

/// <summary>
/// An exception thrown when trying to perform Pythagorean operations on a right 
/// triangle that cannot exist.
/// </summary>
[Serializable]
public class InvalidTriangleException : CalculatorException
{
    public InvalidTriangleException() : base()
    {

    }

    public InvalidTriangleException(string message) : base(message)
    {

    }

    public InvalidTriangleException(string message, Exception inner) : base(message, inner)
    {

    }

    protected InvalidTriangleException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }
}

/// <summary>
/// An exception thrown when inputs passed to the calculator’s evaluation 
/// methods, or a custom operation are invalid. Examples include mismatched 
/// parenthesis or leading zeroes.
/// </summary>
[Serializable]
public class InvalidInputException : CalculatorException
{
    public InvalidInputException() : base()
    {

    }

    public InvalidInputException(string message) : base(message)
    {

    }

    public InvalidInputException(string message, Exception inner) : base(message, inner)
    {

    }

    protected InvalidInputException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }
}

/// <summary>
/// An exception thrown when an infix expression is passed to the calculator 
/// that cannot be evaluated.
/// </summary>
[Serializable]
public class InvalidExpressionException : CalculatorException
{
    public InvalidExpressionException() : base()
    {

    }

    public InvalidExpressionException(string message) : base(message)
    {

    }

    public InvalidExpressionException(string message, Exception inner) : base(message, inner)
    {

    }

    protected InvalidExpressionException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }
}

// <summary>
/// An exception thrown when an expression passed to the calculator contains an 
/// invalid token (i.e., a symbol that is not an operator, or a letter.)
/// </summary>
[Serializable]
public class InvalidTokenException : CalculatorException
{
    public InvalidTokenException() : base()
    {

    }

    public InvalidTokenException(string message) : base(message)
    {

    }

    public InvalidTokenException(string message, Exception inner) : base(message, inner)
    {

    }

    protected InvalidTokenException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {

    }
}