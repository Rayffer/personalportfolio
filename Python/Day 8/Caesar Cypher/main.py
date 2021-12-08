alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']

def encrypt(text: str, shift: int):
    encoded_text = ""
    for letter in text:
        encoded_text += alphabet[(alphabet.index(letter) + shift) % len(alphabet)]
    print(f"The encoded text is {encoded_text}")

print("Welcome to the Caesar Cypher")

text = input("Type your message:\n").lower()
shift = int(input("Type the shift number:\n"))

encrypt(text, shift)