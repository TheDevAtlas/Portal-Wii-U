// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ScreenCutoutShader"
{
	Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PortalRadiusX ("Portal Radius X", Float) = 0.3
        _PortalRadiusY ("Portal Radius Y", Float) = 0.2
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Lighting Off
        Cull Back
        ZWrite On
        ZTest Less
        Fog{ Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _PortalRadiusX;
            float _PortalRadiusY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                

                float2 uv = i.screenPos.xy / i.screenPos.w;
                fixed4 portalCol = tex2D(_MainTex, uv);
                //return portalCol * displayMask + _InactiveColour * (1-displayMask);

                uv = i.uv; // Using the UV passed from the vertex shader
                //fixed4 portalCol = tex2D(_MainTex, uv);
                // Calculate normalized UV coordinates centered at (0.5, 0.5)
                float2 center = float2(0.5, 0.5);
                float2 normUV = (uv - center) * 2.0; // Adjust to range -1 to 1
                // Check if within the ellipse
                float ellipse = (normUV.x * normUV.x) / (_PortalRadiusX * _PortalRadiusX) + (normUV.y * normUV.y) / (_PortalRadiusY * _PortalRadiusY);
                if (ellipse > 1.0)
                    discard; // Discard pixel outside the oval
                return portalCol;
            }
            ENDCG
        }
    }
}
