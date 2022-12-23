import os
import shutil

# Specify the source and destination directories
src_dir = input('Enter the path to the source directory: ')
dst_dir = input('Enter the path to the destination directory: ')

# Walk through all the files and directories in the source directory
for root, dirs, files in os.walk(src_dir):
    # Get the name of the current directory
    dir_name = os.path.basename(root)
    # Copy all the files in the current directory
    for file in files:
        # Construct the new file name by adding the directory name at the start
        new_file_name = dir_name + '_' + file
        src_path = os.path.join(root, file)
        dst_path = os.path.join(dst_dir, new_file_name)
        shutil.copy(src_path, dst_path)

print('Files copied successfully!')
