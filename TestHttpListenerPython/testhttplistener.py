#!/usr/local/bin/python
from argparse import ArgumentParser
from requests_kerberos import HTTPKerberosAuth
import requests

def main(args=None):
    parser = ArgumentParser()
    parser.add_argument("url")
    url = parser.parse_args(args).url
    print("Testing {}".format(url))
    auth = HTTPKerberosAuth()
    resp = requests.get(url, auth=auth)
    print(resp)

if __name__ == "__main__":
    main()
