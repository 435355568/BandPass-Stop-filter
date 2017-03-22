/*
* MATLAB Compiler: 4.18 (R2012b)
* Date: Mon Jan 11 16:46:58 2016
* Arguments: "-B" "macro_default" "-W" "dotnet:addNoise,addNoiseClass,0.0,private" "-T"
* "link:lib" "-d" "D:\Program Files\MATLAB\R2012b\bin\addNoise\src" "-w"
* "enable:specified_file_mismatch" "-w" "enable:repeated_file" "-w"
* "enable:switch_ignored" "-w" "enable:missing_lib_sentinel" "-w" "enable:demo_license"
* "-v" "class{addNoiseClass:D:\matlab\bin\addNoise.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace addNoiseNative
{

  /// <summary>
  /// The addNoiseClass class provides a CLS compliant, Object (native) interface to the
  /// M-functions contained in the files:
  /// <newpara></newpara>
  /// D:\matlab\bin\addNoise.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class addNoiseClass : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Compiler Runtime
    /// instance.
    /// </summary>
    static addNoiseClass()
    {
      if (MWMCR.MCRAppInitialized)
      {
        Assembly assembly= Assembly.GetExecutingAssembly();

        string ctfFilePath= assembly.Location;

        int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

        ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

        string ctfFileName = "addNoise.ctf";

        Stream embeddedCtfStream = null;

        String[] resourceStrings = assembly.GetManifestResourceNames();

        foreach (String name in resourceStrings)
        {
          if (name.Contains(ctfFileName))
          {
            embeddedCtfStream = assembly.GetManifestResourceStream(name);
            break;
          }
        }
        mcr= new MWMCR("",
                       ctfFilePath, embeddedCtfStream, true);
      }
      else
      {
        throw new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the addNoiseClass class.
    /// </summary>
    public addNoiseClass()
    {
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~addNoiseClass()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input Objectinterface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object addNoise()
    {
      return mcr.EvaluateFunction("addNoise", new Object[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input Objectinterface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="I">Input argument #1</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object addNoise(Object I)
    {
      return mcr.EvaluateFunction("addNoise", I);
    }


    /// <summary>
    /// Provides a single output, 2-input Objectinterface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="I">Input argument #1</param>
    /// <param name="NoiseName">Input argument #2</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object addNoise(Object I, Object NoiseName)
    {
      return mcr.EvaluateFunction("addNoise", I, NoiseName);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] addNoise(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "addNoise", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="I">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] addNoise(int numArgsOut, Object I)
    {
      return mcr.EvaluateFunction(numArgsOut, "addNoise", I);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the addNoise M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="I">Input argument #1</param>
    /// <param name="NoiseName">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] addNoise(int numArgsOut, Object I, Object NoiseName)
    {
      return mcr.EvaluateFunction(numArgsOut, "addNoise", I, NoiseName);
    }


    /// <summary>
    /// Provides an interface for the addNoise function in which the input and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// UNTITLED2 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("addNoise", 2, 1, 0)]
    protected void addNoise(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("addNoise", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }

    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
