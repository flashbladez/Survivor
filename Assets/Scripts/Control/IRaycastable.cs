﻿namespace Survivor.Control
{
    public interface IRaycastable
    {
        bool HandleRaycast(PlayerController callingController);
    }
}
