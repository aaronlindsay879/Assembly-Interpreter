﻿namespace Assembly_Interpreter
{
    public enum Opcode
    {
        NONE,
        LDR,
        STR,
        ADD,
        SUB,
        MOV,
        BEQ,
        BNE,
        BGT,
        BLT,
        BGE,
        BLE,
        B,
        AND,
        EOR,
        MVN,
        ORR,
        LSL,
        LSR,
        HALT
    }
}
