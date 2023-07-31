import os
import sys

#count size of all ".cs" files in a directory and subdirectories of a given path
def get_size(path):
	total_size = 0
	for dirpath, dirnames, filenames in os.walk(path):
		for filename in filenames:
			if filename.endswith('.cs') and not "obj" in dirpath and not "bin" in dirpath and not "MaybeLater" in dirpath:
				fp = os.path.join(dirpath, filename)
				total_size += os.path.getsize(fp)
	return total_size

if __name__ == '__main__':
	if len(sys.argv) != 2:
		print('Usage: python get_size.py <path>')
		sys.exit(1)
	path = sys.argv[1]

	size = get_size(path)
	if size > 1000000:
		size = str(size/1000000) + ' MB'
	elif size > 1000:
		size = str(size/1000) + ' KB'
	else:
		size = str(size) + ' B'
	print('Size of all .cs files is ' + size)

#path: C:\Users\llama\Desktop\programmig shit\C#_zapoctak\Graph_Theory_Library\GraphLibrary