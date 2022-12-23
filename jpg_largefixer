import os

# Prompt the user to enter the directory they want to search
directory = input("Enter the filepath of the directory you want to search: ")

# Set the file extensions you want to search for
extensions = ['.jpg_large', '.jpg_medium']

# Search the directory and its subdirectories for files with the specified extensions
for root, dirs, files in os.walk(directory):
    for file in files:
        for extension in extensions:
            if file.endswith(extension):
                # Construct the full file path
                file_path = os.path.join(root, file)
                # Change the file extension
                new_file_path = file_path[:-len(extension)] + '.jpg'
                # Rename the file
                os.rename(file_path, new_file_path)
