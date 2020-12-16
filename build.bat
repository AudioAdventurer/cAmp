set version=v6

docker image build --no-cache -t audioadventurer/camp .

docker image tag audioadventurer/camp audioadventurer/camp:%version%
docker image tag audioadventurer/camp audioadventurer/camp:latest

docker image push audioadventurer/camp:%version%
docker image push audioadventurer/camp:latest