# splatsnap
A mobile app to capture memories using gaussian splatting

## How to install
1. Create the environment with Conda
### Conda environment
conda create --name splatsnap -y python=3.8
conda activate splatsnap
python -m pip install --upgrade pip

### Pytorch with CUDA
pip install torch==2.1.2+cu118 torchvision==0.16.2+cu118 --extra-index-url https://download.pytorch.org/whl/cu118

conda install -c "nvidia/label/cuda-11.8.0" cuda-toolkit

### tiny-cuda-nn
pip install ninja git+https://github.com/NVlabs/tiny-cuda-nn/#subdirectory=bindings/torch

### Install nerfstudio