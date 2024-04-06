'''
A script to generate the template for a LeetCode problem.
'''

import gzip
import pickle
import dataclasses
import sys
import os


@dataclasses.dataclass(slots=True, frozen=True)
class LCP:
    id: int
    difficulty: str
    title: str
    titleCn: str
    titleSlug: str
    paidOnly: bool
    acRate: float
    topicTags: list[str]

    @property
    def url(self):
        return 'https://leetcode.com/problems/' + self.titleSlug

    @property
    def ns(self):
        '''two-sum -> TwoSum'''
        return ''.join(x.capitalize() for x in self.titleSlug.split('-'))

    @property
    def sn(self):
        return str(self.id).zfill(3)

    @property
    def folder(self):
        return self.sn + '.' + self.titleSlug


templateBase = __file__ + '/../'


with gzip.open(templateBase + 'data.pickle.gz') as pickle_in:
    LCPs = pickle.load(pickle_in)

with open(templateBase + 'Readme.md.template') as f:
    readme = f.read()

with open(templateBase + 'Code.cs.template') as f:
    code = f.read()

if __name__ == '__main__':
    if len(sys.argv) != 2:
        print('Usage:', sys.argv[0], '<LeetCode Problem ID>')
        sys.exit(1)

    id = int(sys.argv[1]) - 1
    lcp = LCPs[id]

    readme = readme.replace('<id>', lcp.id).replace('<title>', lcp.title).replace(
        '<URL>', lcp.url).replace('<titleCn>', lcp.titleCn)
    code = code.replace('<SN>', lcp.sn).replace('<NS>', lcp.ns)

    os.mkdir(lcp.folder)
    with open(lcp.folder + '/Readme.md', 'w') as f:
        f.write(readme)
    with open(lcp.folder + '/Code.cs', 'w') as f:
        f.write(code)
