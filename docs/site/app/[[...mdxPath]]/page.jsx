import { generateStaticParamsFor, importPage } from 'nextra/pages'
import { useMDXComponents } from '../../mdx-components'

export const generateStaticParams = generateStaticParamsFor('mdxPath')

export async function generateMetadata({ params }) {
  const { mdxPath } = await params
  const { metadata } = await importPage(mdxPath)
  return metadata
}

const Wrapper = useMDXComponents().wrapper

export default async function Page({ params }) {
  const resolvedParams = await params
  const { default: Content, toc, metadata, sourceCode } = await importPage(resolvedParams.mdxPath)

  return (
    <Wrapper toc={toc} metadata={metadata} sourceCode={sourceCode}>
      <Content params={resolvedParams} />
    </Wrapper>
  )
}