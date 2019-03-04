using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a frame release */
/* The float values are not used by our program, but are needed by SAP */
public class frameRelease : MonoBehaviour
{
    private bool axialStart { get; set; }
    private bool axialEnd { get; set; }
    private float axialStartVal { get; set; }
    private float axialEndVal { get; set; }

    private bool shearMajorStart { get; set; }
    private bool shearMajorEnd { get; set; }
    private float shearMajorStartVal { get; set; }
    private float shearMajorEndVal { get; set; }

    private bool shearMinorStart { get; set; }
    private bool shearMinorEnd { get; set; }
    private float shearMinorStartVal { get; set; }
    private float shearMinorEndVal { get; set; }

    private bool torsionStart { get; set; }
    private bool torsionEnd { get; set; }
    private float torsionStartVal { get; set; }
    private float torsionEndVal { get; set; }

    private bool momentMinorStart { get; set; }
    private bool momentMinorEnd { get; set; }
    private float momentMinorStartVal { get; set; }
    private float momentMinorEndVal { get; set; }

    private bool momentMajorStart { get; set; }
    private bool momentMajorEnd { get; set; }
    private float momentMajorStartVal { get; set; }
    private float momentMajorEndVal { get; set; }

	public frameRelease()
	{
        axialStart = true;
        axialEnd = true;
        axialStartVal = 0f;
        axialEndVal = 0f;

        shearMajorStart = false;
        shearMajorEnd = false;
        shearMajorStartVal = 0f;
        shearMajorEndVal = 0f;

        shearMinorStart = false;
        shearMinorEnd = false;
        shearMinorStartVal = 0f;
        shearMinorEndVal = 0f;

        torsionStart = false;
        torsionEnd = false;
        torsionStartVal = 0f;
        torsionEndVal = 0f;

        momentMinorStart = false;
        momentMinorEnd = false;
        momentMinorStartVal = 0f;
        momentMinorEndVal = 0f;

        momentMajorStart = false;
        momentMajorEnd = false;
        momentMajorStartVal = 0f;
        momentMajorEndVal = 0f;
	}

    /// <summary>
    /// Returns whether the release is completely released at the start
    /// This is needed when changing the visual appearence of frames (if they are pulled back from the node or not)
    /// </summary>
    public bool isReleaseStart() {
        if(!axialStart && !shearMajorStart && !shearMinorStart && !torsionStart && !momentMinorStart && !momentMajorStart) {
            return false;
        } else {
            return true;
        }
    }

    /// <summary>
    /// Returns whether the release is completely released at the end
    /// This is needed when changing the visual appearence of frames (if they are pulled back from the node or not)
    /// </summary>
    public bool isReleaseEnd() {
        if(!axialEnd && !shearMajorEnd && !shearMinorEnd && !torsionEnd && !momentMinorEnd && !momentMajorEnd) {
            return false;
        } else {
            return true;
        }
    }
}
