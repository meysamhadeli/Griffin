import nextra from 'nextra'

const withNextra = nextra({ defaultShowCopyCode: true })

export default withNextra({
  output: 'export',
  basePath: process.env.DOCS_BASE_PATH || '',
  trailingSlash: true,
  images: { unoptimized: true }
})