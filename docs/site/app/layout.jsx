import { Footer, Layout, Navbar } from 'nextra-theme-docs'
import { Head } from 'nextra/components'
import { getPageMap } from 'nextra/page-map'
import 'nextra-theme-docs/style.css'

export const metadata = {
  title: { default: 'Griffin Documentation', template: '%s | Griffin' },
  description: 'Composable .NET building blocks for production-ready web services.'
}

export default async function RootLayout({ children }) {
  const basePath = process.env.DOCS_BASE_PATH || ''

  return (
    <html lang="en" dir="ltr" suppressHydrationWarning>
      <Head />
      <body>
        <Layout
          navbar={
            <Navbar
              logo={<><img src={`${basePath}/griffin-logo.png`} alt="" width="36" height="36" /><strong>Griffin</strong></>}
              projectLink="https://github.com/meysamhadeli/Griffin"
            />
          }
          pageMap={await getPageMap()}
          editLink={null}
          feedback={{ content: null }}
          footer={<Footer>Griffin. MIT License.</Footer>}
        >
          {children}
        </Layout>
      </body>
    </html>
  )
}