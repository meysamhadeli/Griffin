import { copyFile, mkdir, readFile, readdir, writeFile } from 'node:fs/promises'

const root = new URL('../../../', import.meta.url)
const site = new URL('../', import.meta.url)
const repository = 'https://github.com/meysamhadeli/Griffin'
const readme = await readFile(new URL('README.md', root), 'utf8')
const guideStart = readme.indexOf('## What Griffin Provides')

if (guideStart === -1) {
  throw new Error('README.md must contain the What Griffin Provides section.')
}

await mkdir(new URL('content/', site), { recursive: true })
await mkdir(new URL('public/', site), { recursive: true })

const guide = '# Griffin\n\nReusable .NET building blocks for production-ready web services.\n\n' +
  readme.slice(guideStart)
    .replaceAll('(./CONTRIBUTION.md)', `(${repository}/blob/main/CONTRIBUTION.md)`)
    .replaceAll('(LICENSE)', `(${repository}/blob/main/LICENSE)`)

await writeFile(new URL('content/index.mdx', site), guide)
await copyFile(new URL('docs/how-it-works.md', root), new URL('content/architecture.mdx', site))
await copyFile(new URL('CONTRIBUTION.md', root), new URL('content/contributing.mdx', site))
await copyFile(new URL('assets/griffin-logo.png', root), new URL('public/griffin-logo.png', site))

await mkdir(new URL('content/modules/', site), { recursive: true })

for (const filename of await readdir(new URL('docs/modules/', root))) {
  if (filename.endsWith('.md')) {
    await copyFile(
      new URL(`docs/modules/${filename}`, root),
      new URL(`content/modules/${filename.replace(/\.md$/, '.mdx')}`, site)
    )
  }
}