set version=v8

docker image build --no-cache -t audioadventurer/camp .

docker image tag audioadventurer/camp audioadventurer/camp:%version%
docker image tag audioadventurer/camp audioadventurer/camp:latest