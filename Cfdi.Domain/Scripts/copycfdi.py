import shutil

def addZeros(number, quantity = 4):
    n = quantity + 1 - len(str(number))
    return ("0" * n) + str(number) if n > 0 else str(number)
    
i = 1
while(i < 15000): #60000/4 = 15000
    shutil.copy2('C:\\CFDIs\\invoice00001.xml', f'C:\\CFDIs\\invoice{addZeros(1 + i*4)}.xml')
    shutil.copy2('C:\\CFDIs\\invoice00002.xml', f'C:\\CFDIs\\invoice{addZeros(2 + i*4)}.xml')
    shutil.copy2('C:\\CFDIs\\invoice00003.xml', f'C:\\CFDIs\\invoice{addZeros(3 + i*4)}.xml')
    shutil.copy2('C:\\CFDIs\\invoice00004.xml', f'C:\\CFDIs\\invoice{addZeros(4 + i*4)}.xml')
    i += 1
