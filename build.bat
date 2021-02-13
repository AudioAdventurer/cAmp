set version=v9

docker image build --no-cache -t audioadventurer/camp .

docker image tag audioadventurer/camp audioadventurer/camp:%version%
docker image tag audioadventurer/camp audioadventurer/camp:latest