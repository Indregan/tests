<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="/">
    <html>
      <body>
        <table border="1">
      <xsl:apply-templates />
        </table>
      </body>
    </html>
  </xsl:template>

  <!-- Evaluate Elements -->
  <xsl:template match="node()[name()]">
    <tr bgcolor="#9acd32">
      <th>
        <xsl:value-of select="name()" />
      </th>
     </tr>
      <th bgcolor="#9acd92">
        <xsl:apply-templates select="@* | node()"/>
      </th>
  </xsl:template>

  <!-- Evaluate Attributes -->
  <xsl:template match="@*">
    <tr bgcolor="#9acd32">
      <th>
        <xsl:value-of select="name()" />
      </th>
    </tr>
      <th>
        <xsl:value-of select="." />
      </th>
  </xsl:template>
  
</xsl:stylesheet>