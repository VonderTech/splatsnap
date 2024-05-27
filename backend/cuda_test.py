from torch import cuda
from nerfstudio.scripts import process_data

print(cuda.is_available())
print(cuda.device_count())
print(cuda.current_device())
print(cuda.get_device_name(cuda.current_device()))

process_data()
