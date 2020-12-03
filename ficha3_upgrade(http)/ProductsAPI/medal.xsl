<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:template match="/*">
    <table cellpadding="0" cellspacing="0" border="1">
      <tr style="color: green;">
        <th>Country</th>
        <th>Gold</th>
        <th>Silver</th>
        <th>Bronze</th>
      </tr>
      <xsl:for-each select="Country">
        <tr style="text-align: center;">
          <td>
            <xsl:value-of select="@Name"></xsl:value-of>
          </td>
          <td>
            <xsl:value-of select="@Gold"></xsl:value-of>
          </td>
          <td>
            <xsl:value-of select="@Silver"></xsl:value-of>
          </td>
          <td>
            <xsl:value-of select="@Bronze"></xsl:value-of>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
