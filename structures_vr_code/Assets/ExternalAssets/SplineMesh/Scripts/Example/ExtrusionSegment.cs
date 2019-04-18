﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SplineMesh {
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class ExtrusionSegment : MonoBehaviour {
        private MeshFilter mf;
        private Mesh result;
        private List<Vertex> shapeVertices = new List<Vertex>();
        private CubicBezierCurve curve;
        private float textureScale = 1;
        private float textureOffset = 0;

        private void OnEnable() {
            mf = GetComponent<MeshFilter>();
            if (mf.sharedMesh == null) {
                mf.sharedMesh = new Mesh();
            }
        }

        /// <summary>
        /// Set the cubic Bézier curve to use to bend the source mesh, and begin to listen to curve control points for changes.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="update">If let to true, update the resulting mesh immediatly.</param>
        public void SetCurve(CubicBezierCurve curve, bool update = true) {
            if (this.curve != null) {
                this.curve.Changed.RemoveListener(() => Compute());
            }
            this.curve = curve;
            curve.Changed.AddListener(() => Compute());
            if (update) Compute();
        }

        /// <summary>
        /// Set the source mesh.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="update">If let to true, update the resulting mesh immediatly.</param>
        public void SetShapeVertices(List<Vertex> shapeVertices, bool update = true) {
            this.shapeVertices = shapeVertices;
            if (update) Compute();
        }

        public void SetTextureScale(float textureScale, bool update = true) {
            this.textureScale = textureScale;
            if (update) Compute();
        }

        public void SetTextureOffset(float textureOffset, bool update = true) {
            this.textureOffset = textureOffset;
            if (update) Compute();
        }

        private List<OrientedPoint> GetPath() {
            var path = new List<OrientedPoint>();
            CurveSample sample;
            for (float t = 0; t < 1; t += 1 / 30.0f) {
                sample = curve.GetSample(t);
                path.Add(new OrientedPoint() {
                    position = sample.location,
                    rotation = sample.Rotation,
                    scale = sample.scale,
                    roll = sample.roll,
                    distanceInCurve = sample.distanceInCurve
                });
            }
            sample = curve.GetSample(1);
            path.Add(new OrientedPoint() {
                position = sample.location,
                rotation = sample.Rotation,
                scale = sample.scale,
                roll = sample.roll,
                distanceInCurve = sample.distanceInCurve
            });
            return path;
        }

        public void Compute() {
            List<OrientedPoint> path = GetPath();

            int vertsInShape = shapeVertices.Count;
            int segments = path.Count - 1;
            int edgeLoops = path.Count;
            int vertCount = vertsInShape * edgeLoops;

            var triangleIndices = new List<int>(vertsInShape * 2 * segments * 3);
            var vertices = new Vector3[vertCount];
            var normals = new Vector3[vertCount];
            var uvs = new Vector2[vertCount];

            int index = 0;
            foreach (OrientedPoint op in path) {
                foreach (Vertex v in shapeVertices) {
                    var position = v.point;
                    // apply scale
                    position = Vector3.Scale(position, new Vector3(op.scale.x, op.scale.y, 0));

                    // apply roll
                    position = Quaternion.AngleAxis(op.roll, Vector3.forward) * position;

                    vertices[index] = op.LocalToWorld(position);
                    normals[index] = op.LocalToWorldDirection(v.normal);
                    uvs[index] = new Vector2(v.uCoord, textureScale * (op.distanceInCurve + textureOffset)); index++;
                }
            }
            index = 0;
            for (int i = 0; i < segments; i++) {
                for (int j = 0; j < shapeVertices.Count; j++) {
                    int offset = j == shapeVertices.Count - 1 ? -(shapeVertices.Count - 1) : 1;
                    int a = index + shapeVertices.Count;
                    int b = index;
                    int c = index + offset;
                    int d = index + offset + shapeVertices.Count;
                    triangleIndices.Add(c);
                    triangleIndices.Add(b);
                    triangleIndices.Add(a);
                    triangleIndices.Add(a);
                    triangleIndices.Add(d);
                    triangleIndices.Add(c);
                    index++;
                }
            }

            mf.sharedMesh.Clear();
            mf.sharedMesh.vertices = vertices;
            mf.sharedMesh.normals = normals;
            mf.sharedMesh.uv = uvs;
            mf.sharedMesh.triangles = triangleIndices.ToArray();
        }

        [Serializable]
        public class Vertex {
            public Vector2 point;
            public Vector2 normal;
            public float uCoord;

            public Vertex(Vector2 point, Vector2 normal, float uCoord) {
                this.point = point;
                this.normal = normal;
                this.uCoord = uCoord;
            }
        }

        public struct OrientedPoint {
            public Vector3 position;
            public Quaternion rotation;
            public Vector2 scale;
            public float roll;
            public float distanceInCurve;

            public Vector3 LocalToWorld(Vector3 point) {
                return position + rotation * point;
            }

            public Vector3 LocalToWorldDirection(Vector3 dir) {
                return rotation * dir;
            }
        }

    }
}
