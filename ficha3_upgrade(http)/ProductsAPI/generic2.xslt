<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> 
  
<xsl:template match="/">

<HTML>
<BODY>
    <xsl:apply-templates/>
</BODY>
</HTML>

</xsl:template>

<xsl:template match="/*">
<TABLE BORDER="1">
<TR>
        <xsl:for-each select="*[position() = 1]/*">
          <TD>
              <xsl:value-of select="local-name()"/>
          </TD>
        </xsl:for-each>
</TR>
      <xsl:apply-templates/>
</TABLE>
</xsl:template>

<xsl:template match="/*/*">
<TR>
    <xsl:for-each select="*">
      <td>
        <xsl:apply-templates select="." />
      </td>
  </xsl:for-each>
</TR>
</xsl:template>

<xsl:template match="image">
   <img src="{.}" /> 
</xsl:template>

<xsl:template match="linkref">
  <a href="{.}">
    <xsl:value-of select="." />
  </a>
</xsl:template>
</xsl:stylesheet>