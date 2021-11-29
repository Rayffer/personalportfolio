for number in range(1, 101):
    string_to_print = ("Fizz" if number % 3 == 0 else "") + ("Buzz" if number % 5 == 0 else "")
    print(f'{string_to_print if string_to_print != "" else number}')