# Assembly-Interpreter

This is a simple project which emulates a processor with a defineable amount of memory addresses and registers.

Instructions are executed on the falling edge, which is shown as being executed as the next line number is highlighted.


### Supported commands
* Simpe instructions
    * LDR R*d*,[operand] - Loads the value in [operand] into register _d_
    * STR R*d*,[memory ref] - Stores the value in register _d_ into [memory ref]
    * ADD R*d*,R*n*,[operand] - Adds the value in R*n* to [operand] and stores the result in register _d_
    * SUB R*d*,R*n*,[operand] - Subtracts the value in R*n* from [operand] and stores the result in register _d_
    * MOV R*d*,R*n* - Copies the value in R*n* into register _d_
    * HALT - Stops execution of the program
* Branch instructions
    * B [operand] - Branches to the line stored in [operand]
    * BEQ [operandOne],[operandTwo],[operandThree] - Branches to the line [operandThree] if [operandOne] == [operandTwo]
    * BNE - Branch if not equal (arguments as above)
    * BGT - Branch if greater than (arguments as above)
    * BGE - Branch if greater than or equal to (arguments as above)
    * BLT - Branch if less than (arguments as above)
    * BLE - Branch if less than or equal to (arguments as above)
* Bitwise instructions
    * AND R*d*,R*n*,[operand] - Performs an and operation between R*n* and [operand], stores result in register _d_
    * ORR - OR (arguments as above)
    * EOR - XOR (arguments as above)
    * MVN R*d*,[operand]- Performs NOT on [operand], stores result in register _d_
    * LSL R*d*,R*n*,[operand] - Shifts R*n* left by [operand], stores result in register _d_
    * LSR - Shifts right (arguments asabove)
